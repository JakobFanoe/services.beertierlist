using System.ComponentModel.DataAnnotations;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.api.Controllers.Tierlist.Requests;

public class UpdateTierlistEntriesRequest
{
    [Required]
    [MaxLength(1000)]
    public required List<TierlistEntry> Updates { get; set; }
}
