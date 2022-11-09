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
    await this.getVaultKeeps(vkData.vaultId)
    const keep = AppState.vaultkeeps.find(k => k.id == AppState.activeKeep.id)
    if (keep) {
      Pop.toast("You've already added that keep to this vault!", "warning")
      return
    }
    vkData.keepId = AppState.activeKeep.id
    await api.post("/api/vaultkeeps", vkData)
  }

  async removeVaultKeep(keepId, vaultId) {
    const vk = AppState.vaultkeeps.find(k => k.keepId == keepId && k.vaultId == vaultId)
    if (!vk) {
      console.error("Could not find vaultkeep")
    }
    if (AppState.account.id != AppState.activeVault.creatorId) {
      Pop.error("You can only delete keeps from your own vault")
    }
    await api.delete(`/api/vaultkeeps/${vk.vaultKeepId}`)
    AppState.vaultkeeps = AppState.vaultkeeps.filter(k => k.id != vk.id)
  }
}

export const vaultKeepsService = new VaultKeepsService()