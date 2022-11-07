namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _profilesRepository;

  public ProfilesService(ProfilesRepository profilesRepository)
  {
    _profilesRepository = profilesRepository;
  }

  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _profilesRepository.GetProfileById(profileId);
    if (profile == null)
    {
      throw new Exception("That profile does not exist");
    }
    return profile;
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    Profile profile = GetProfileById(profileId);
    List<Keep> keeps = _profilesRepository.GetKeepsByProfileId(profile);
    return keeps;
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    Profile profile = GetProfileById(profileId);
    List<Vault> vaults = _profilesRepository.GetVaultsByProfileId(profileId);
    List<Vault> filtered = vaults.FindAll(v => v.IsPrivate == false);
    filtered.ForEach(v =>
    {
      v.Creator = profile;
      v.CreatorId = profileId;
    });
    return filtered;
  }
}