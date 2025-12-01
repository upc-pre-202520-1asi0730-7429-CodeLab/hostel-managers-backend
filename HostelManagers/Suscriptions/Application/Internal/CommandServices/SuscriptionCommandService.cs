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
        // 1. Verificar el pago con el servicio externo
        var paymentApproved = await _payPalService.IsPaymentApprovedAsync(command.PayPalTransactionId);
    
        // 2. Determinar el estado basado en la verificación (Lógica del Dominio)
        // Usamos el estado calculado, no el que viene en el comando.
        var statusDetermined = paymentApproved ? Status.Suscrito : Status.NoSuscrito;
    
        // 3. Crear el agregado Suscription.
        // **Ajuste Importante:**
        // - Debes incluir el `UserId` (asumiendo que está en tu comando, por ejemplo, `command.UserId`).
        // - Debes usar la variable de estado `statusDetermined` y no `command.Statu`.
        var suscription = new Suscription(
            command.UserId, // <<-- Necesitas agregar UserId al comando si no lo tiene
            command.Plan, 
            command.PayPalTransactionId, 
            statusDetermined // <--- ¡USAR ESTE VALOR CALCULADO!
        );

        // 4. Persistir y confirmar
        await _suscriptionRepository.AddAsync(suscription);
        await _unitOfWork.CompleteAsync(); 
    
        return suscription;
    }
}