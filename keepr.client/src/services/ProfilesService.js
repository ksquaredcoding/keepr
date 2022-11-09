import { AppState } from "../AppState.js"
import { Profile } from "../models/Account.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"

class ProfilesService {
  async getProfileVaults() {
    const res = await api.get(`/api/profiles/${AppState.activeProfile.id}/vaults`)
    const vaults = res.data.map(v => new Vault(v))
    AppState.profileVaults = vaults.filter(v => v.isPrivate == false)
  }

  async getActiveProfile(profileId) {
    const res = await api.get(`/api/profiles/${profileId}`)
    AppState.activeProfile = new Profile(res.data)
  }
}

export const profilesService = new ProfilesService()