using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contract.ScheduledJobs
{
  public interface IScheduledJobService
  {
    #region Fire And Forget

    string Enqueue(Expression<Action> functionCall);
    string Enqueue<T>(Expression<Action<T>> functionCall);

    #endregion

    #region Delayed Jobs

    string Schedule(Expression<Action> functionCall, TimeSpan delay);
    // delay trong khoảng thời gian nào đó
    string Schedule<T>(Expression<Action<T>> functionCall, TimeSpan delay);
    string Schedule(Expression<Action> functionCall, DateTimeOffset enqueueAt);
    // chạy đúng thời điểm nào đó
    // string Schedule<T>(Expression<Action<T>> functionCall, DateTimeOffset enqueueAt);

    #endregion

    #region Continuos Jobs

    string ContinueQueueWith(string parentJobId, Expression<Action> functionCall);
    // chạy 2 job liên tiếp nhau 

    #endregion

    bool Delete(string jobId);
    //xóa job

    bool Requeue(string jobId);
    // chạy lại job
  }
}
