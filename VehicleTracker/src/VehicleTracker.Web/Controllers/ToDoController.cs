using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Core;
using VehicleTracker.Core.Entities;
using VehicleTracker.SharedKernel.Interfaces;
using VehicleTracker.Web.ApiModels;

namespace VehicleTracker.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IRepository _repository;

        public ToDoController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var items = (await _repository.ListAsync<ToDoItem>())
                            .Select(ToDoItemDTO.FromToDoItem);
            return View(items);
        }

        public IActionResult Populate()
        {
            int recordsAdded = DatabasePopulator.PopulateDatabase(_repository);
            return Ok(recordsAdded);
        }
    }
}
