import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("/api/keeps")
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async setActiveKeep(keepId) {
    AppState.activeKeep = null
    const activeKeep = AppState.keeps.find(k => k.id == keepId)
    if (!activeKeep) {
      activeKeep = await api.get(`/api/keeps/${keepId}`)
      AppState.activeKeep = activeKeep
    }
    AppState.activeKeep = activeKeep
  }

  async createKeep(keepData) {
    const res = await api.post("/api/keeps", keepData)
    const keep = new Keep(res.data)
    AppState.keeps = [keep, ...AppState.keeps]
    AppState.activeKeep = null
    AppState.activeKeep = keep
  }
}

export const keepsService = new KeepsService()