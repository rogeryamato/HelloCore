using Autofac;
using Castle.DynamicProxy;
using HelloCore.Common;
using HelloCore.Interface;
using HelloCore.Interface.Manually;
using HelloCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using WebExtension;

namespace HelloCore
{
    public class UnitOfWorkInterceptionBehaviorBase : IInterceptor
    {
        private IComponentContext componentContext;

        public UnitOfWorkInterceptionBehaviorBase(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public void Intercept(IInvocation invocation)
        {
            if(ContextUnitOfWork.Current !=null ||
                IsDBConnectionRequired(invocation.Method) ==false)
            {
                invocation.Proceed();
                return;
            }

            var isolationLevel = IsolationLevel.ReadUncommitted;
            try
            {

                ContextUnitOfWork.Current =
                    new Lazy<IContextUnitOfWork>(() => GetUnitOfWork(),
                    LazyThreadSafetyMode.PublicationOnly);


                if (!NeedSaveChanges(invocation.Method))
                    invocation.Proceed();

                if(NeedTransaction(invocation.Method))
                {
                    isolationLevel = IsolationLevel.ReadUncommitted;
                    ContextUnitOfWork.Current.Value.BeginTransaction(isolationLevel);

                    invocation.Proceed();

                    ContextUnitOfWork.Current.Value.Commit();
                }
                else
                {
                    invocation.Proceed();
                    if(NeedSaveChanges(invocation.Method))
                    {
                        ContextUnitOfWork.Current.Value.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                if (NeedTransaction(invocation.Method))
                    ContextUnitOfWork.Current.Value.Rollback();
                throw e;
            }
            finally
            {
                //ContextUnitOfWork.Current.Value.Dispose();
                ContextUnitOfWork.Current = null;
            }
        }

        private bool IsDBConnectionRequired(MethodBase methodInfo)
        {
            bool required = false;
            required = IsRepositoryMethod(methodInfo) || IsUnitOfWorkAttributed(methodInfo);
            return required;
        }
        
        private bool IsRepositoryMethod(MethodBase methodInfo)
        {
            return methodInfo.DeclaringType.Name.ToLower().Contains("repository");
        }

        private bool IsUnitOfWorkAttributed(MethodBase methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        private bool NeedSaveChanges(MethodBase methodInfo)
        {
            return CommonConstant.NeedSaveChangesMethodNames.Any(n => n.Contains(methodInfo.Name));
        }

        private bool NeedTransaction(MethodBase methodInfo)
        {
            return IsUnitOfWorkAttributed(methodInfo);
        }

        private ContextUnitOfWork GetUnitOfWork()
        {
            return new ContextUnitOfWork(componentContext.Resolve<IDbContext>());
        }

    }
}
