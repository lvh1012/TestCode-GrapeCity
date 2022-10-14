using Microsoft.AspNetCore.Mvc;
using TestCode.Entities;
using TestCode.Services.Interfaces;

namespace TestCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public UserController(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            var response = ResponseObject<Azure.AsyncPageable<UserEntity>>.ReturnWithData(users);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserEntity entity)
        {
            string Id = Guid.NewGuid().ToString();
            entity.Id = Id;
            entity.RowKey = Id;
            entity.PartitionKey = "user";
            var createdEntity = await _userService.Create(entity);

            MailContent content = new MailContent
            {
                To = entity.Email,
                Subject = "Xin chào",
                Body = "<p><strong>Tôi là Lê Viết Hoàng</strong></p>"
            };

            // asynchronous
            _mailService.SendMail(content);

            var response = ResponseObject<UserEntity>.ReturnWithData(createdEntity);
            return Ok(response);
        }
    }

    public class ResponseObject<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ResponseObject(T data)
        {
            Success = true;
            Data = data;
        }

        public ResponseObject(string message)
        {
            Success = false;
            Message = message;
        }

        public static ResponseObject<T> ReturnWithData(T data)
        {
            return new ResponseObject<T>(data);
        }

        public static ResponseObject<T> ReturnWithError(string message)
        {
            return new ResponseObject<T>(message);
        }
    }
}
