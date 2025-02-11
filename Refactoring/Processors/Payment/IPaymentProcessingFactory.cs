using Refactoring.Enums;

namespace Refactoring.Processors.Payment
{
    public interface IPaymentProcessingFactory
    {
        IPaymentProcessor GetProcessor(PaymentType paymentType);
    }
}
