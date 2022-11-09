import { AppState } from "../AppState.js"
import { VaultKept } from "../models/Keep.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async getVaultKeeps(vaultId) {
    const res = await api.get(`/api/vaults/${vaultId}/keeps`)
    AppState.vaultkeeps = res.data.map(k => new VaultKept(k))
  }

  async addVaultKeep(vkData) {
    const keeps = await this.getVaultKeeps(vkData.vaultId)
    const keep = keeps.find(k => k.id == AppState.activeKeep.id)
    if (keep) {
      Pop.toast("You've already added that keep to this vault!", "warning")
      return
    }
    vkData.vaultId = vaultData
    vkData.keepId = AppState.activeKeep.id
    await api.post("/api/vaultkeeps", vkData)
  }
}

export const vaultKeepsService = new VaultKeepsService()