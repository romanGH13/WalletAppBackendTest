using Microsoft.AspNetCore.Mvc;
using WalletAppBackend.Entities;
using WalletAppBackend.Models;
using WalletAppBackend.Services;

namespace WalletAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IUserService userService;

        public TransactionController(ITransactionService transactionService, IUserService userService)
        {
            this.transactionService = transactionService;
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Transaction? transaction = transactionService.GetTransactionById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpGet("list/{userId}/{page}/{pageCount}")]
        public IActionResult GetUsers(int userId, int page = 1, int pageCount = 10)
        {
            PagedResponse<Transaction> response = transactionService.GetTransactionsByUserId(userId, page, pageCount);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(AddTransactionRequest request)
        {
            User? user = await userService.GetById(request.UserId);

            if (user == null)
            {
                BadRequest(nameof(request.UserId));
            }

            if (request.AuthorizedUserId != null)
            {
                User? authorizedUser = await userService.GetById(request.UserId);
                if (authorizedUser == null)
                {
                    BadRequest(nameof(request.AuthorizedUserId));
                }
            }

            var transaction = await transactionService.AddTransaction(request.Name, request.Status, request.Type, request.Sum,
                request.UserId, request.Description, request.AuthorizedUserId);

            AddTransactionResponse response = new AddTransactionResponse() { Id = transaction.Id };

            return Ok(response);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Transaction? transactionToDelete = transactionService.GetTransactionById(id);

            if (transactionToDelete == null)
                NotFound("Transaction not found");

            transactionService.DeleteTransaction(transactionToDelete!);

            return Ok();
        }
    }
}
