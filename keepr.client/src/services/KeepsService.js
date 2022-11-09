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
    const res = await api.get(`/api/keeps/${keepId}`)
    AppState.activeKeep = new Keep(res.data)
  }

  async createKeep(keepData) {
    const res = await api.post("/api/keeps", keepData)
    const keep = new Keep(res.data)
    AppState.keeps = [...AppState.keeps, keep]
    AppState.activeKeep = null
    AppState.activeKeep = keep
  }

  async getProfileKeeps(profileId) {
    const res = await api.get(`/api/profiles/${profileId}/keeps`)
    AppState.profileKeeps = res.data.map(k => new Keep(k))
  }
}

export const keepsService = new KeepsService()