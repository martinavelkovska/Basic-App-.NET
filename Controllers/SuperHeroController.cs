using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using Yt_App.Data;
using Yt_App.Entities;
//all logic here....
namespace Yt_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        //create get method returning a superhero witho no database at all
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            //var heroes = new List<SuperHero>
            //{
            //    new SuperHero
            //    {
            //        Id=1,
            //        Name = "Spiderman",
            //        FirstName = "Peter",
            //        LastName = "Parker",
            //        Place = "New York City"
            //    }
            //};
            //return Ok(heroes); // to retrun status code 200 OK

            var heroes = await _dataContext.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
           _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(hero.Id);
            if(dbHero == null)
            {
                return BadRequest("Hero not found");
            }

            dbHero.Name =  hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());

        }

        [HttpDelete]

        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if(hero is null)
            {
                return BadRequest("Hero not found.");
            }

            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

    } 
}
