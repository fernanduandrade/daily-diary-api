namespace DailyDiary.Domain.Common;

public interface IRepository<T> where T : class, IAggregateRoot { }