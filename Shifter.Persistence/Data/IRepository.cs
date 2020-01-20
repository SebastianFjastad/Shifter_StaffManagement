using Framework.Notifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shifter.Persistence.Data
{
    public interface IRepository
    {
        NotificationCollection Save(object entity);

        NotificationCollection Save(IEnumerable<object> entities);

        IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAll<T>();

        T FindById<T>(int id);

        NotificationCollection Delete<T>(int id);
    }
}
