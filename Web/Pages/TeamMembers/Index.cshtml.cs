using Application.DTOs;
using Application.Mappings;
using Application.Services.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels.TeamMembers;

namespace Web.Pages.TeamMembers;

public class IndexModel : PageModel
{
    private readonly ITeamMemberService _teamMemberService;

    public IndexModel(ITeamMemberService teamMemberService)
    {
        _teamMemberService = teamMemberService;
    }

    public ICollection<TeamMemberDto> TeamMembers { get; set; } = new List<TeamMemberDto>();

    [BindProperty(SupportsGet = true)] public string? SearchName { get; set; }
    [BindProperty(SupportsGet = true)] public Role? SearchRole { get; set; }
    [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    public IEnumerable<SelectListItem> RoleOptions { get; set; }

    public string GetRoleDisplay(string tecnicalRole)
    {
        if (Enum.TryParse<Role>(tecnicalRole, out var role))
        {
            return role.ToFriendlyString();
        }
        return tecnicalRole;
    }

    public void ChargeRoleOptions()
    {
        RoleOptions = Enum.GetValues<Role>()
            .Select(role => new SelectListItem
            {
                Value = role.ToString(),
                Text = role.ToFriendlyString()
            })
            .ToList();
    }

    public async Task ChargeTeamMembersAsync()
    {
        var result = await _teamMemberService.GetAll(
            SearchName,
            SearchRole,
            CurrentPage,
            PageSize);

        TeamMembers = result.Items;
        
        TotalRecords = result.TotalRecords;
    }

    public async Task<IActionResult> OnGetAsync(bool isPartial = false)
    {
        ChargeRoleOptions();
        await ChargeTeamMembersAsync();
        if (isPartial)
        {
            return Partial("_TeamMemberList", this);
        }
        return Page();
    }
    
    #region Modales
    public PartialViewResult OnGetCreateModal()
    {
        var viewModel = new TeamMemberFormViewModel();
        return Partial("_TeamMemberModal", viewModel);
    }
    
    public async Task<PartialViewResult> OnGetDeleteModal(Guid id)
    {
        var result = await _teamMemberService.GetById(id);
        return Partial("_DeleteMemberModal", result.Value);
    }
    
    public async Task<PartialViewResult> OnGetEditModal(Guid id)
    {
        var result = await _teamMemberService.GetById(id);
        var member = result.Value;
        
        var viewModel = new TeamMemberFormViewModel
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            Role = Enum.Parse<Role>(member.Role)
        };

        return Partial("_TeamMemberModal", viewModel);
    }
    #endregion

    #region Acciones
    public async Task<IActionResult> OnPostCreate(CreateTeamMemberDto input)
    {
        var result = await _teamMemberService.Add(input);
        if (result.IsSuccess) return new JsonResult(new { success = true });

        return new JsonResult(new
        {
            success = result.IsSuccess, 
            message = result.Message,
            errors = result.ErrorsValidations
        });
    }
    
    public async Task<IActionResult> OnPostEdit(UpdateTeamMemberDto input)
    {
        var result = await _teamMemberService.Update(input);
        if (result.IsSuccess) return new JsonResult(new { success = true });

        return new JsonResult(new
        {
            success = result.IsSuccess, 
            message = result.Message,
            errors = result.ErrorsValidations
        });
    }
    
    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        var result = await _teamMemberService.Delete(id);
    
        return new JsonResult(new { 
            success = result.IsSuccess, 
            message = result.Message,
            errors = result.ErrorsValidations
        });
    }
    #endregion
}