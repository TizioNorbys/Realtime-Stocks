using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocks.API.Models.Requests.Stocks;
using Stocks.Application.DTOs.Stocks;
using Stocks.Application.Errors.Stocks;
using Stocks.Application.Errors.Validation;
using Stocks.Application.Services.Interfaces;
using StocksApi.Extensions;

namespace StocksApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StocksController : ControllerBase
{
	private readonly IStocksService _stocksService;

	public StocksController(IStocksService stocksService)
	{
		_stocksService = stocksService;
	}

	[Authorize]
	[HttpGet("[action]")]
	public async Task<IActionResult> CurrentPrice([FromQuery] string symbol)
	{
		var result = await _stocksService.GetCurrentPrice(symbol.ToUpper());
		if (result.IsFailed)
			return BadRequest(new { result.GetFirstError().Message });

		return Ok(result.Value);
	}

	[Authorize]
	[HttpPost("[action]/{symbol}")]
	public async Task<IActionResult> HistoricalData([FromBody] HistoricalDataRequest request, string symbol)
	{
		HistoricalDataDto requestDto = new(request.StartDate, request.EndDate);

		var result = await _stocksService.GetHistoricalData(symbol.ToUpper(), requestDto);
		if (result.IsFailed)
		{
			return result.GetFirstError() switch
			{
				ValidationError err => ValidationProblem(ModelState.AddValidationErrors(err.Metadata)),
				HistoricalSearchError err => NotFound(new { err.Message })
			};
		}

		return Ok(result.Value);
	}
}