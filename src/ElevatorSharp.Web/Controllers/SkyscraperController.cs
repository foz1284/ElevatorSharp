using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ElevatorSharp.Domain;
using ElevatorSharp.Domain.DataTransferObjects;
using ElevatorSharp.Game;
using ElevatorSharp.Tests.Players;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;

namespace ElevatorSharp.Web.Controllers
{
    public class SkyscraperController : Controller
    {
        public ActionResult Index(string message = null, string source = null, string diagnostics = null)
        {
            ViewBag.Message = message;
            ViewBag.Source = string.IsNullOrWhiteSpace(source) || !MemoryCache.Default.Contains(source) 
                ? _defaultCode 
                : Encoding.Default.GetString(Convert.FromBase64String((string)MemoryCache.Default.Get(source)));

            if (diagnostics != null && diagnostics.Any())
            {
                ViewBag.Diagnostics = Encoding.Default.GetString(Convert.FromBase64String(diagnostics)).Split('±');
            }
            else
            {
                ViewBag.Diagnostics = new string[0];
            }
            return View();
        }

        private string _defaultCode = @"using System;
using ElevatorSharp.Game;

namespace ElevatorSharp.Default
{
    public class TestPlayer : IPlayer
    {
        public void Init(IElevator[] elevators, IFloor[] floors)
        {
            // Let's use the first elevator
            var elevator = elevators[0];

            // Whenever the elevator is idle (has no more queued destinations) ...
            elevator.Idle += ElevatorOnIdle;
        }
        
        private void ElevatorOnIdle(object sender, EventArgs eventArgs)
        {
            var elevator = (IElevator) sender;

            // let's go to all the floors (or did we forget one?)
            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
        }

        public void Update(IElevator[] elevators, IFloor[] floors)
        {
            // We normally don't need to do anything here
        }
    }
}";

        public ActionResult UploadPlayer(HttpPostedFileBase dll)
        {
            var message = "Could not load assembly";
            if (dll == null || dll.ContentLength <= 0) return RedirectToAction("Index", new {message});

            // Load player dll
            var fileName = $"{Guid.NewGuid()},{Path.GetFileName(dll.FileName)}";
            var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
            dll.SaveAs(path);
            var playerAssembly = Assembly.LoadFile(path);
            return FindPlayer(playerAssembly);
        }

        private ActionResult FindPlayer(Assembly playerAssembly, string source = null)
        {
            string message;
            foreach (var type in playerAssembly.GetTypes().OrderBy(t => t.Name))
            {
                if (type.GetInterface("IPlayer") != null)
                {
                    var player = Activator.CreateInstance(type) as IPlayer;
                    SavePlayer(player);
                    message = type.Name + " uploaded.";
                    return RedirectToAction("Index", new {message, source });
                }
            }
            return RedirectToAction("Index", new {message= "No player implementing IPlayer found." });
        }

        [ValidateInput(false)]
        public ActionResult UploadPlayerAsCode(string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var references = new[]
            {
                MetadataReference.CreateFromFile(typeof (object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof (LambdaExpression).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location),
                MetadataReference.CreateFromFile(typeof(IPlayer).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IPlayer).Assembly.Location)
            };
            var compilation = CSharpCompilation.Create("IPlayer", new[] { syntaxTree }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            
            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);
                var sourceGuid = Guid.NewGuid().ToString();
                MemoryCache.Default.Add(sourceGuid, Convert.ToBase64String(Encoding.Default.GetBytes(source)), new CacheItemPolicy {AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)});
                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var dll = Assembly.Load(ms.ToArray());
                    return FindPlayer(dll, sourceGuid);
                }

                return RedirectToAction("Index", new { message = "Compilation issues", source = sourceGuid, diagnostics = Convert.ToBase64String(Encoding.Default.GetBytes(string.Join("±", result.Diagnostics))) });
            }
        }

        public ContentResult New(SkyscraperDto skyscraperDto)
        {
            var skyscraper = new Skyscraper(skyscraperDto);
            var player = LoadPlayer();
            skyscraper.LoadPlayer(player); // This calls the Init method on Player and hooks up events
            SaveSkyscraper(skyscraper);

            var json = JsonConvert.SerializeObject("Ready."); // TODO: Not needed, unless you want to pass some UI message back.
            return Content(json, "application/json");
        }

        #region Helper Methods
        private static void SaveSkyscraper(Skyscraper skyscraper)
        {
            var cache = MemoryCache.Default;
            cache.Remove("skyscraper");
            cache.Set("skyscraper", skyscraper, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) });
        }

        private static void SavePlayer(IPlayer player)
        {
            var cache = MemoryCache.Default;
            cache.Set("player", player, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) });
        }

        private static IPlayer LoadPlayer()
        {
            var cache = MemoryCache.Default;
            if (cache.Contains("player"))
            {
                return (IPlayer)cache.Get("player");
            }
            return new DevPlayer();
        }
        #endregion  
    }
}