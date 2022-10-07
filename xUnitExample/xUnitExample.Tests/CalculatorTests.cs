using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnitExample.Tests {
  public class CalculatorTests {
    [Fact]
    public void Add_() {
      double Expected = 20;
      var Actual = Calculator.Add(5, 15);
      Assert.Equal(Expected, Actual);
    }
  }
}
