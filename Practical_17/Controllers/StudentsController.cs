using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practical_17.Models;
using Practical_17.ViewModels;

namespace Practical_17.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public StudentsController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _client = httpClientFactory.CreateClient("StudentApiClient");
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<Student>>("/api/studentsApi");
            return View(_mapper.Map<List<StudentViewModel>>(response));
        }

        [Authorize(Roles = "User")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _client.PostAsJsonAsync("/api/studentsApi", _mapper.Map<Student>(model));
                if (result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _client.GetFromJsonAsync<Student>($"/api/studentsApi/{id}");
            if (student == null) return NotFound();
            return View(_mapper.Map<StudentViewModel>(student));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var result = await _client.PutAsJsonAsync($"/api/studentsApi/{id}", _mapper.Map<Student>(model));
                if (result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _client.GetFromJsonAsync<Student>($"/api/studentsApi/{id}");
            if (student == null) return NotFound();
            return View(_mapper.Map<StudentViewModel>(student));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _client.GetFromJsonAsync<Student>($"/api/studentsApi/{id}");
            if (student == null) return NotFound();
            return View(_mapper.Map<StudentViewModel>(student));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _client.DeleteAsync($"/api/studentsApi/{id}");
            return RedirectToAction(nameof(Index));
        }
    }

}
