import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { router } from "../router.js"
import Pop from "../utils/Pop.js"
import { accountService } from "./AccountService.js"
import { api } from "./AxiosService.js"
import { vaultKeepsService } from "./VaultKeepsService.js"

class VaultsService {
  async createVault(vaultData) {
    const res = await api.post("/api/vaults", vaultData)
    const vault = new Vault(res.data)
    AppState.vaults.push(vault)
  }

  async getActiveVault(vaultId) {
    const vault = await this.getVaultById(vaultId)
    if (vault.isPrivate == true && vault.creatorId != AppState.account.id) {

      Pop.toast("That vault is private, sorry!", "warning")
      return
    }
    if (!vault) {
      Pop.toast("Unable to find vault, sorry!", "warning")
      return
    }
    await vaultKeepsService.getVaultKeeps(vaultId)
    AppState.activeVault = vault
  }

  async getVaultById(vaultId) {
    const res = await api.get(`/api/vaults/${vaultId}`)
    const vault = new Vault(res.data)
    if (!vault) {
      console.error("Could not find vault")
    }
    return vault
  }

  async removeVault(vaultId) {
    const vault = await this.getVaultById(vaultId)
    if (vault.creatorId != AppState.account.id) {
      Pop.error("You can only delete your own vaults")
    }
    await api.delete(`/api/vaults/${vaultId}`)
    router.push({ name: "Home" })
  }

  async editVault(vaultData) {
    const vault = await this.getVaultById(AppState.activeVault.id)
    if (vault.creatorId != AppState.account.id) {
      Pop.error("You can only edit your own vaults")
    }
    const res = await api.put(`/api/vaults/${vault.id}`, vaultData)
    const newVault = new Vault(res.data)
    AppState.activeVault = newVault
    accountService.getMyVaults()
  }
}

export const vaultsService = new VaultsService()