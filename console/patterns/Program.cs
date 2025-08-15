static decimal CalcularFrete(Entrega entrega) =>
    entrega switch
    {
        RetiradaNaLoja => 0m,
        EntregaExpressa(var km) when km < 20 => 15m,
        EntregaExpressa(var km) => 25m + (decimal)(km - 20) * 1.5m,
        EntregaAgendada { DataAgendada: var data } when data.DayOfWeek == DayOfWeek.Sunday => 50m,
        EntregaAgendada => 20m,
        _ => throw new ArgumentException("Tipo de entrega desconhecido.")
    };

public abstract record Entrega;

public record RetiradaNaLoja() : Entrega;

public record EntregaExpressa(double DistanciaKm) : Entrega;

public record EntregaAgendada(DateTime DataAgendada) : Entrega;    