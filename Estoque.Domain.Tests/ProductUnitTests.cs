using Estoque.Domain.Entities;
using Xunit;

namespace Estoque.Domain.Tests
{
    public class ProductUnitTests
    {
        private Product _productMoc;

        public ProductUnitTests()
        {
            _productMoc = new("Hyundai Creta 2.0", 130000);
        }

        [Fact]
        public void Validate_Control()
        {
            Assert.True(_productMoc.IsValid());
        }

        [Fact]
        public void Validate_EmptyId()
        {
            _productMoc.Id = Guid.Empty;
            Assert.False(_productMoc.IsValid());
        }

        [Fact]
        public void Validate_NullName()
        {
            _productMoc.Update(null, 130000);
            Assert.False(_productMoc.IsValid());
        }

        [Fact]
        public void Validate_EmptyName()
        {
            _productMoc.Update(string.Empty, 130000);
            Assert.False(_productMoc.IsValid());
        }

        [Fact]
        public void Validate_Name_MaxLength()
        {
            _productMoc.Update("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff", 130000);
            Assert.False(_productMoc.IsValid());
        }

        [Fact]
        public void Validate_ZeroValue()
        {
            _productMoc.Update("Hyundai Creta 2.0", decimal.Zero);
            Assert.False(_productMoc.IsValid());
        }
    }
}
