using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoldenCityApiTest.Controllers
{
    [Route("notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static List<Note> notes = new List<Note>();
        private static int nextid = 1;

        //– Create a new note
        [HttpPost]
        public IActionResult Create(Note note)
        {
            note.Id = nextid++;
            notes.Add(note);
            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);

        }

        //– Retrieve all notes
        [HttpGet]
        public IActionResult GetAll() => Ok(notes);

        //– Retrieve a specific note by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var note = notes.FirstOrDefault(n => n.Id == id);
            if (note == null) return NotFound();
            return Ok(note);

        }

        // – Update a note by ID
        [HttpPut("{id}")]
        public IActionResult Update(int id,Note UpdateNote)
        {
            var note = notes.FirstOrDefault(n => n.Id == id);
            if (note==null)return NotFound();
            note.Name = UpdateNote.Name;
            note.Description = UpdateNote.Description;
            return Ok(note);


        }

        //– Delete a note by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) 
        {
            var note = notes.FirstOrDefault(n => n.Id == id);
                if(note==null) return NotFound();

                notes.Remove(note);
                return NoContent();
        }

    }
}
