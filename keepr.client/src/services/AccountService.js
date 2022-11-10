import { AppState } from '../AppState'
import { Account } from "../models/Account.js"
import { Vault } from "../models/Vault.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyVaults() {
    const res = await api.get("/account/vaults")
    AppState.vaults = res.data.map(v => new Vault(v))
  }

  async getMyVaultKeeps() {
    const res = await api.get("/account/vaultkeeps")
    AppState.myVaultKeeps = res.data.map(v => new VaultKeep(v))
  }

  async editAccount(accountData) {
    const res = await api.put("/account", accountData);
    AppState.account = new Account(res.data);
  }
}

export const accountService = new AccountService()
