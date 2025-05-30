﻿using System.Linq.Expressions;
using Core.Entities;

namespace Core.DataAccess;

public interface IEntityRepository<T> where T : class, IEntity, new()
{
    List<T> GetAll(Expression<Func<T, bool>>? filter = null);
    T Get(Expression<Func<T, bool>>? filter);
    void Add(T car);
    void Delete(T car);
    void Update(T car);
}