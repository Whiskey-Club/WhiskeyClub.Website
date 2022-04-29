using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhiskeyClub.Website.Domain;

/// <summary>
/// Defines a review aggregate.
/// </summary>
public class Review
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Review" /> class.
    /// </summary>
    /// <param name="id">The review identifier.</param>
    /// <param name="spirit">The spirit information.</param>
    public Review(string id, SpiritInfo spirit, string authorId, string authorName)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
        }

        this.Id = id;
        this.Spirit = spirit ?? throw new ArgumentNullException(nameof(spirit));
        this.AuthorId = authorId;
        this.AuthorName = authorName;
    }

    /// <summary>
    /// Gets the review identifier.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; }

    /// <summary>
    /// Gets the spirit information.
    /// </summary>
    [JsonProperty("spirit")]
    public SpiritInfo Spirit { get; }

    /// <summary>
    /// Gets the rating, out of 5.
    /// </summary>
    [JsonProperty("rating")]
    public int Rating { get; } 

    /// <summary>
    /// Gets the notes for the review.
    /// </summary>
    [JsonProperty("notes")]
    public string Notes { get; } = string.Empty;

    /// <summary>
    /// Gets the author identifier.
    /// </summary>
    [JsonProperty("authorId")]
    public string AuthorId { get; }

    /// <summary>
    /// Gets the author's name.
    /// </summary>
    /// <value></value>
    [JsonProperty("authorName")]
    public string AuthorName { get; }

    /// <summary>
    /// Gets the date the review was created.
    /// </summary>
    [JsonProperty("created")]
    public DateTime Created { get; }

    /// <summary>
    /// Gets the review identifier
    /// </summary>
    /// <remarks>Used as the partition key for the Reviews CosmosDB container.</remarks>
    [JsonProperty("reviewId")]
    private string reviewId => this.Id;

    /// <summary>
    /// Gets the spirit identifier.
    /// </summary>
    /// <remarks>Used as the partition key for the Spirits CosmosDB container.</remarks>
    [JsonProperty("spiritId")]
    private string spiritId => this.Spirit?.Id ?? string.Empty;

    /// <summary>
    /// Gets the user identifier of the author.
    /// </summary>
    /// <remarks>Used as the partition key for the Users CosmosDB container.</remarks>
    [JsonProperty("userId")]
    private string userId => this.AuthorId;

    /// <summary>
    /// Gets the document type.
    /// </summary>
    /// <remarks>Used to identify the type of document in the CosmosDB container.</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("type")]
    private DocumentType DocumentType => DocumentType.Review;
}