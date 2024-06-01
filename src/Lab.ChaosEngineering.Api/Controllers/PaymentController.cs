using Lab.ChaosEngineering.AppServices.Dtos.Payments.Create;
using Lab.ChaosEngineering.AppServices.Dtos.Payments.Update;
using Lab.ChaosEngineering.AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab.ChaosEngineering.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentAppService _paymentAppService;
		private readonly ILogger<PaymentController> _logger;

		public PaymentController(IPaymentAppService paymentAppService, ILogger<PaymentController> logger)
		{
			_paymentAppService = paymentAppService;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO createPaymentDTO)
		{
			if (createPaymentDTO == null)
			{
				return BadRequest("Invalid payment data.");
			}

			try
			{
				var paymentDTO = await _paymentAppService.CreatePaymentAsync(createPaymentDTO);
				return CreatedAtAction(nameof(GetPaymentById), new { paymentId = paymentDTO.PaymentId }, paymentDTO);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while creating a payment.");
				return StatusCode(500, "Internal server error.");
			}
		}

		[HttpGet("{paymentId}")]
		public async Task<IActionResult> GetPaymentById(Guid paymentId)
		{
			try
			{
				var paymentDTO = await _paymentAppService.GetPaymentByIdAsync(paymentId);
				if (paymentDTO == null)
				{
					return NotFound();
				}

				return Ok(paymentDTO);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while getting payment with ID {paymentId}.");
				return StatusCode(500, "Internal server error.");
			}
		}

		[HttpPut("{paymentId}")]
		public async Task<IActionResult> UpdatePayment(Guid paymentId, [FromBody] UpdatePaymentDTO updatePaymentDTO)
		{
			if (updatePaymentDTO == null)
			{
				return BadRequest("Invalid payment data.");
			}

			try
			{
				var paymentDTO = await _paymentAppService.UpdatePaymentAsync(paymentId, updatePaymentDTO);
				if (paymentDTO == null)
				{
					return NotFound();
				}

				return Ok(paymentDTO);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while updating payment with ID {paymentId}.");
				return StatusCode(500, "Internal server error.");
			}
		}
	}
}