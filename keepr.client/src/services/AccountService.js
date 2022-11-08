import { AppState } from '../AppState'
import { Vault } from "../models/Vault.js"
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
}

export const accountService = new AccountService()
