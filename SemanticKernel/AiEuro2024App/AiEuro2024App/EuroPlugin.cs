using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;

namespace AiEuro2024App;

public class EuroPlugin
{
    private static readonly List<Team> Teams =
    [
        new Team { Id = 1, Name = "Spain", WorldRanking = 8 },
        new Team { Id = 2, Name = "Georgia", WorldRanking = 74 },
        new Team { Id = 3, Name = "Germany", WorldRanking = 16 },
        new Team { Id = 4, Name = "Denmark", WorldRanking = 21 },
        new Team { Id = 5, Name = "Portugal", WorldRanking = 6 },
        new Team { Id = 6, Name = "Slovenia", WorldRanking = 57 },
        new Team { Id = 7, Name = "France", WorldRanking = 2 },
        new Team { Id = 8, Name = "Belgium", WorldRanking = 3 },
        new Team { Id = 9, Name = "Romania", WorldRanking = 47 },
        new Team { Id = 10, Name = "Netherlands", WorldRanking = 7 },
        new Team { Id = 11, Name = "Austria", WorldRanking = 25 },
        new Team { Id = 12, Name = "Turkey", WorldRanking = 41 },
        new Team { Id = 13, Name = "England", WorldRanking = 5 },
        new Team { Id = 14, Name = "Slovakia", WorldRanking = 45 },
        new Team { Id = 15, Name = "Switzerland", WorldRanking = 19 },
        new Team { Id = 16, Name = "Italy", WorldRanking = 10 },
        new Team { Id = 17, Name = "San Marino", WorldRanking = 210 }
    ];

    private static readonly List<Match> Last16Matches =
    [
        new Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2 },
        new Match { Id = 2, HomeTeamId = 3, AwayTeamId = 4 },
        new Match { Id = 3, HomeTeamId = 5, AwayTeamId = 6 },
        new Match { Id = 4, HomeTeamId = 7, AwayTeamId = 8 },
        new Match { Id = 5, HomeTeamId = 9, AwayTeamId = 10 },
        new Match { Id = 6, HomeTeamId = 11, AwayTeamId = 12 },
        new Match { Id = 7, HomeTeamId = 13, AwayTeamId = 14 },
        new Match { Id = 8, HomeTeamId = 15, AwayTeamId = 16 }
    ];

    private static readonly List<Match> QuarterFinals =
    [
        new Match { Id = 9, MatchIdToDetermineHomeTeam = 1, MatchIdToDetermineAwayTeam = 2 },
        new Match { Id = 10, MatchIdToDetermineHomeTeam = 3, MatchIdToDetermineAwayTeam = 4 },
        new Match { Id = 11, MatchIdToDetermineHomeTeam = 5, MatchIdToDetermineAwayTeam = 6 },
        new Match { Id = 12, MatchIdToDetermineHomeTeam = 7, MatchIdToDetermineAwayTeam = 8 }
    ];

    private static readonly List<Match> SemiFinals =
    [
        new Match { Id = 13, MatchIdToDetermineHomeTeam = 9, MatchIdToDetermineAwayTeam = 10 },
        new Match { Id = 14, MatchIdToDetermineHomeTeam = 11, MatchIdToDetermineAwayTeam = 12 }
    ];

    private static readonly List<Match> Final =
        [new Match { Id = 15, MatchIdToDetermineHomeTeam = 13, MatchIdToDetermineAwayTeam = 14 }];

    private static readonly List<Match> AllMatches =
        Last16Matches.Concat(QuarterFinals).Concat(SemiFinals).Concat(Final).ToList();

    [KernelFunction("get_teams")]
    [Description("Gets a list of all teams playing at Euro 2024 including their world ranking")]
    [return: Description("An array of teams")]
    public async Task<IReadOnlyCollection<Team>> GetTeamsAsync() => Teams;
    
    [KernelFunction("get_matches")]
    [Description("Gets a list of all matches still to be played at Euro 2024 referencing the ids of the teams that will be playing or the match id of the corresponding knockout game which will determine the winner")]
    [return: Description("An array of matches")]
    public async Task<IReadOnlyCollection<Match>> GetMatchesAsync() => AllMatches;

    //public async Task<Team> GetTeamById(int id) => Teams.First(x => x.Id == id);
    //public async Task<Match> GetMatchById(int id) => AllMatches.First(x => x.Id == id);
}

public struct Match
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("home_team_id")] public int? HomeTeamId { get; set; }
    [JsonPropertyName("match_id_to_determine_home_team")] public int? MatchIdToDetermineHomeTeam { get; set; }
    [JsonPropertyName("away_team_id")] public int? AwayTeamId { get; set; }
    [JsonPropertyName("match_id_to_determine_away_team")] public int? MatchIdToDetermineAwayTeam { get; set; }
}

public struct Team
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("world_ranking")] public int WorldRanking { get; set; }
}