import api from "../api/api";
import { Categoria } from "../types";

export const listarCategorias = async (): Promise<Categoria[]> => {
  const response = await api.get("/categorias");
  return response.data;
};

export const criarCategoria = async (categoria: Omit<Categoria, 'id'>): Promise<Categoria> => {
  const response = await api.post("/categorias", categoria);
  return response.data;
};

export const atualizarCategoria = async (id: string, categoria: Omit<Categoria, 'id'>): Promise<Categoria> => {
  const response = await api.put(`/categorias/${id}`, categoria);
  return response.data;
};

export const deletarCategoria = async (id: string): Promise<void> => {
  await api.delete(`/categorias/${id}`);
};