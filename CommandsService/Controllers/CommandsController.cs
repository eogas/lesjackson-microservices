using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepository repository;
    private readonly IMapper mapper;

    public CommandsController(ICommandRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
        Console.WriteLine($"--> Hit GetCommandsFormPlatform: {platformId}");

        if (!repository.PlatformExists(platformId))
        {
            return NotFound();
        }

        var commands = repository.GetCommandsForPlatform(platformId);
        var dtos = mapper.Map<IEnumerable<CommandReadDto>>(commands);

        return Ok(dtos);
    }

    [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
        Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

        if (!repository.PlatformExists(platformId))
        {
            return NotFound();
        }

        var command = repository.GetCommand(platformId, commandId);

        if (command == null)
        {
            return NotFound();
        }

        var dto = mapper.Map<CommandReadDto>(command);

        return Ok(dto);
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
    {
        Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

        if (!repository.PlatformExists(platformId))
        {
            return NotFound();
        }

        var command = mapper.Map<Command>(commandDto);
        repository.CreateCommand(platformId, command);
        repository.SaveChanges();

        var commandReadDto = mapper.Map<CommandReadDto>(command);

        return CreatedAtRoute(nameof(GetCommandForPlatform),
            new
            {
                platformId = platformId,
                commandId = commandReadDto.Id
            },
        commandReadDto);
    }
}
