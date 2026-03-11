import api from "../api/api";
import { Pessoa } from "../types";

export const listarPessoas = async (): Promise<Pessoa[]> => {
  const response = await api.get("/pessoas");
  return response.data;
};

export const criarPessoa = async (pessoa: Omit<Pessoa, 'id'>): Promise<Pessoa> => {
  const response = await api.post("/pessoas", pessoa);
  return response.data;
};

export const atualizarPessoa = async (id: string, pessoa: Omit<Pessoa, 'id'>): Promise<Pessoa> => {
  const response = await api.put(`/pessoas/${id}`, pessoa);
  return response.data;
};

export const deletarPessoa = async (id: string): Promise<void> => {
  await api.delete(`/pessoas/${id}`);
};