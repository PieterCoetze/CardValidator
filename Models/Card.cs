using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardValidator.Models;

public partial class Card
{
    public int CardId { get; set; }

    public int CardProviderId { get; set; }

    public string CardNumber { get; set; } = null!;

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime? CreatedDate { get; set; }

    public virtual CardProvider CardProvider { get; set; } = null!;
}
