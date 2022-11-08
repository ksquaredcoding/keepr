import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"

class VaultsService {
  async createVault(vaultData) {
    const res = await api.post("/api/vaults", vaultData)
    const vault = new Vault(res.data)
    AppState.vaults.push(vault)
  }
}

export const vaultsService = new VaultsService()