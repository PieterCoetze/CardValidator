using System;
using System.Collections.Generic;

namespace CardValidator.Models;

public partial class Card
{
    public int CardId { get; set; }

    public int CardProviderId { get; set; }

    public string CardNumber { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public virtual CardProvider CardProvider { get; set; } = null!;
}
