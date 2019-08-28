using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimpleCalculatorAPI.Models
{
    public class CalculatorModel
    {
        public float[] Operands { get; set; }
        public float Result { get; set; }
        public CalculatorOperations Operation { get; set; }

        public void calculate()
        {
            if (Operands == null)
            {
                Result = 0;
                return;
            }

            switch (Operation)
            {
                case CalculatorOperations.Add:
                    Result = Operands.Aggregate((f1, f2) => f1 + f2);
                    break;
                case CalculatorOperations.Sub:
                    Result = Operands.Aggregate((f1, f2) => f1 - f2);
                    break;
                case CalculatorOperations.Mul:
                    Result = Operands.Aggregate((f1, f2) => f1 * f2);
                    break;
                case CalculatorOperations.Div:
                    Result = Operands.Aggregate((f1, f2) => f1 / f2);
                    break;
            }
        }
    }

    public enum CalculatorOperations
    {
        Add,
        Sub,
        Mul,
        Div
    }
}