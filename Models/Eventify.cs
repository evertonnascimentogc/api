namespace Eventify.Models;

public class EventifyItem
{
  public long Id { get; set; }
  public int Prioridade { get; set; }
  public string? Nome { get; set; }
  public string? Descricao { get; set; }
  public DateTime DataDeEntrega { get; set; }
}