using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MysqlWebAPI.DTO;
using MysqlWebAPI.Entities;
using System.Linq;
using System.Net;

namespace MysqlWebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public UserController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var list = await _dbContext.Users.Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                CreatedTs = x.CreatedTs,
                //    Password = x.Password
            }).ToListAsync();
            return list;
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            UserDTO user = await _dbContext.Users.Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                CreatedTs = x.CreatedTs,
                //    Password = x.Password
            }).FirstOrDefaultAsync(o => o.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost("AddUser")]
        public async Task<HttpStatusCode> InsertUser(UserAddDTO user)
        {
            var entity = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreatedTs = DateTime.Now
            };
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserUpdateDTO user)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == user.Id);
            if (entity == null)
            {
                return HttpStatusCode.NotFound;
            }
            entity.Name = user.Name;
            entity.Password = user.Password;
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<HttpStatusCode> DeleteUser(int id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == id);
            if (entity == null)
            {
                return HttpStatusCode.NotFound;
            }
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
