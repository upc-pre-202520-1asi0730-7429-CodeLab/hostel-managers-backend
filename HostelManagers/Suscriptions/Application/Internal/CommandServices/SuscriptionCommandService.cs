using HostelManagers.Shared.Domain.Repositories;
using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using HostelManagers.Suscriptions.Domain.Model.Commands;
using HostelManagers.Suscriptions.Domain.Model.ValueObjets;
using HostelManagers.Suscriptions.Domain.Repositories;
using HostelManagers.Suscriptions.Domain.Services;

namespace HostelManagers.Suscriptions.Application.Internal.CommandServices;

public class SuscriptionCommandService : ISuscriptionCommandService
{
    private readonly ISuscriptionRepository _suscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPayPalService _payPalService;

    public SuscriptionCommandService(
        ISuscriptionRepository suscriptionRepository,
        IUnitOfWork unitOfWork,
        IPayPalService payPalService)
    {
        _suscriptionRepository = suscriptionRepository;
        _unitOfWork = unitOfWork;
        _payPalService = payPalService;
    }

    public async Task<Suscription?> Handle(CreateSuscriptionCommand command)
    {
        var paymentApproved = await _payPalService.IsPaymentApprovedAsync(command.PayPalTransactionId);
        var status = paymentApproved ? Status.Suscrito : Status.NoSuscrito;
        var suscription = new Suscription(command.Plan, command.PayPalTransactionId, command.Statu);

        await _suscriptionRepository.AddAsync(suscription);
        await _unitOfWork.CompleteAsync(); 
        return suscription;
    }
}