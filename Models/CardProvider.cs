using System;
using System.Collections.Generic;

namespace CardValidator.Models;

public partial class CardProvider
{
    public int CardProviderId { get; set; }

    public string CardProviderName { get; set; } = null!;

    public bool? Configured { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Card> TCards { get; set; } = new List<Card>();
}
