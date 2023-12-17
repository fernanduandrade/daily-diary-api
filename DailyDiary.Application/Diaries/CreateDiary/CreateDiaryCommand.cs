using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using OneOf;
using MediatR;

namespace DailyDiary.Application.Diaries.CreateDiary;

public sealed record CreateDiaryCommand(
    Guid userId,
    string Title,
    string Text,
    string Mood,
    bool IsPublic) : IRequest<OneOf<ApiResponse<DiaryDto>, Error>>;