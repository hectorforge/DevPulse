using Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels.Incidents;

public class AssignMemberViewModel
{
    public AssignIncidentRequest Request { get; set; } = null!;
    public IEnumerable<SelectListItem> TeamMemberOptions { get; set; } = new List<SelectListItem>();
    public string IncidentTitle { get; set; } = string.Empty;
}