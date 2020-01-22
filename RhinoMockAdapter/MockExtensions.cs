using System;
using System.Linq.Expressions;
using Moq.Language.Flow;

namespace RhinoMockAdapter
{
    public static class MockExtensions
    {
        public static IExpect<T,R> Expect<T,R>(this T obj, Expression<Func<T, R>> expression) where T: class
        {
            return Stub(obj, expression);
        }

        public static IExpect<T> Expect<T>(this T obj, Expression<Action<T>> expression) where T: class
        {
            return Stub(obj, expression);
        }

        public static IExpect<T,R> Stub<T,R>(this T obj, Expression<Func<T, R>> expression) where T: class
        {
            return new Expect<T,R>(MockRepository.GetMockOf(obj).Setup(expression));
        }

        public static IExpect<T> Stub<T>(this T obj, Expression<Action<T>> expression) where T: class
        {
            return new Expect<T>(MockRepository.GetMockOf(obj).Setup(expression));
        }

        public static void VerifyAllExpectations<T>(this T obj) where T: class
        {
            MockRepository.GetMockOf(obj).VerifyAllExpectations();
        }
    }

    public interface IExpect<T>
    {
        void Throw(Exception exception);
    }

    public interface IExpect<T,R>
    {
        void Return(R result);
    }

    public class Expect<T, R> : IExpect<T, R> where T : class
    {
        private ISetup<T, R> setup;

        public Expect(ISetup<T, R> setup)
        {
            this.setup = setup;
        }

        public void Return(R result)
        {
            setup.Returns(result);
        }
    }

    public class Expect<T> : IExpect<T> where T : class
    {
        private ISetup<T> setup;

        public Expect(ISetup<T> setup)
        {
            this.setup = setup;
        }

        public void Throw(Exception exception)
        {
            throw new NotImplementedException();
        }
    }

    public class Arg<T>
    {
        public static ArgIs<T> Is { get; set; }
    }

    public class ArgIs<T>
    {
        public string Anything { get; set; }

        public T Equal(T other)
        {
            throw new NotImplementedException();
        }
    }
}