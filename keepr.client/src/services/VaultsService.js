import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { router } from "../router.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"
import { vaultKeepsService } from "./VaultKeepsService.js"

class VaultsService {
  async createVault(vaultData) {
    const res = await api.post("/api/vaults", vaultData)
    const vault = new Vault(res.data)
    AppState.vaults.push(vault)
  }

  async setActiveVault(vaultId) {
    const vault = AppState.vaults.find(v => v.id == vaultId)
    if (!vault) {
      await this.getActiveVault(vaultId)
    }
    if (vault.isPrivate && vault.creatorId != AppState.account.id) {
      router.push({ to: "Home" })
      Pop.toast("That vault is private, sorry!", "warning")
    }
    AppState.activeVault = vault
    await vaultKeepsService.getVaultKeeps(vaultId)
  }

  async getActiveVault(vaultId) {
    const res = await api.get(`/api/vaults/${vaultId}`)
    const vault = new Vault(res.data)
    if (vault.isPrivate && vault.creatorId != AppState.account.id) {
      router.push({ to: "Home" })
      Pop.toast("That vault is private, sorry!", "warning")
      return
    }
    if (!vault) {
      router.push({ to: "Home" })
      Pop.toast("Unable to find vault, sorry!", "warning")
      return
    }
    await vaultKeepsService.getVaultKeeps(vaultId)
    AppState.activeVault = vault
  }
}

export const vaultsService = new VaultsService()