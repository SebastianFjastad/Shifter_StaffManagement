using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Notifications;

namespace Framework.Domain
{
    public interface IRepository : IDisposable
    {
        NotificationCollection Save(object entity);

        NotificationCollection Save(IEnumerable<object> entities);

        IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAll<T>();

        T FindById<T>(int id);

        NotificationCollection Delete<T>(int id);
    }
}
