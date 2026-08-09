using Application.DTOs;
using Domain.Common;

namespace Application.Services.Interfaces;

public interface IPostMortemService
{
    Task<(ICollection<PostMortemDto> Items, int TotalRecords)> GetAll(string rootCause, int page = 1, int size = 10);
    Task<Result<PostMortemDto>> Add(CreatePostDto dto);
    Task<Result<PostMortemDto?>> Update(UpdatePostDto dto);
    Task<Result<PostMortemDto?>> Delete(Guid id);
    Task<Result<PostMortemDto?>> GetById(Guid id);
}