using Moq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PayrollEstimator.Tests
{
    public static class EntityFrameworkHelper
    {
        public static Mock<DbSet<TEntity>> GetMockDbSet<TEntity>(IQueryable<TEntity> entities) where TEntity : class
        {
            var mockSet = new Mock<DbSet<TEntity>>();
            mockSet.As<IDbAsyncEnumerable<TEntity>>().Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(entities.GetEnumerator()));
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TEntity>(entities.Provider));
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);
            return mockSet;
        }
    }
}
