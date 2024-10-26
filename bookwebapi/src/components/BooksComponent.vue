<template>
  <div class="container">
    <h1 class="title">Libros</h1>
    <button class="button" @click="fetchBooks">Listar Libros</button>
    <table class="book-table">
      <thead>
        <tr>
          <th>Titulo</th>
          <th>Descripcion</th>
          <th>Numero de Paginas</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in books" :key="book.id" class="book-item">
          <td>{{ book.title }}</td>
          <td>{{ book.description }}</td>
          <td>{{ book.pageCount }}</td>
          <td class="book-actions">
            <button class="action-button" @click="GetBooksById(book.id)">ver detalle</button>
            <button class="action-button delete-button" @click="deleteBook(book.id)">Eliminar</button>
          </td>
        </tr>
      </tbody>
    </table>
    <button class="button create-button" @click="CreateForm = true">Crear Libro</button>
    
    <div v-if="CreateForm" class="form-container">
      <h2 class="form-title">{{ editingBook ? 'Editar Libro' : 'Crear Libro' }}</h2>
      <input class="form-input" v-model="newBook.title" placeholder="Titulo" />
      <textarea class="form-textarea" v-model="newBook.description" placeholder="Descripcion"></textarea>
      <input class="form-input" v-model="newBook.pageCount" type="number" placeholder="Numero de Paginas" />
      <input class="form-input" v-model="newBook.excerpt" placeholder="Extracto" />
      <input class="form-input" v-model="newBook.publishDate" type="date" placeholder="Fecha de Publicacion" />
      <button class="button submit-button" @click="editingBook ? updateBook(editingBook.id) : createBook()">
        {{ editingBook ? 'Actualizar' : 'Crear' }}
      </button>
      <button class="button cancel-button" @click="cancelForm">Cancelar</button>
    </div>

    <div v-if="bookDetails" class="details-container">
      <h2 class="details-title">Detalle del Libro</h2>
      <p><strong>Titulo:</strong> {{ bookDetails.title }}</p>
      <p><strong>Descripcion:</strong> {{ bookDetails.description }}</p>
      <p><strong>Numero de Paginas:</strong> {{ bookDetails.pageCount }}</p>
      <p><strong>Extracto:</strong> {{ bookDetails.excerpt }}</p>
      <p><strong>Fecha de Publicacion:</strong> {{ bookDetails.publishDate }}</p>
    </div>
  </div>
</template>

<script>
import api from '../api';

export default {
  name: 'BooksComponent',
  data() {
    return {
      books: [],
      bookDetails: null,
      CreateForm: false,
      editingBook: null,
      newBook: { 
        title: '', 
        description: '', 
        pageCount: 0, 
        excerpt: '', 
        publishDate: '' 
      }
    };
  },
  methods: {
    async fetchBooks() {
      try {
        const response = await api.get('/books');
        console.info(response);
        if (response.status === 200) this.books = response.data;
        else alert('Error al listar los libros');
      } catch (error) { alert('Error en el servidor'); }
    },
    async GetBooksById(id) {
      try {
        const response = await api.get(`/books/${id}`);
        console.info('Respuesta del servidor:', response);
        if (response.status === 200) {
          this.bookDetails = response.data;
          this.editingBook = this.bookDetails; 
          this.newBook = { ...this.bookDetails };
          this.CreateForm = true; 
        }
      } catch (error) { alert('Error en el servidor'); }
    },
    async createBook() {
      try {
        const response = await api.post('/books', this.newBook);
        if (response.status === 201) {
          alert('Libro creado con éxito');
          this.fetchBooks();
          this.resetForm();
        } else alert('Error al crear el libro');
      } catch (error) { alert('Error en el servidor'); }
    },
    async updateBook(id) {
      try {
        const response = await api.put(`/books/${id}`, this.newBook);
        if (response.status === 204) {
          alert('Libro actualizado con éxito');
          this.fetchBooks();
          this.resetForm();
        } else alert('Error al actualizar el libro');
      } catch (error) { alert('Error en el servidor'); }
    },
    async deleteBook(id) {
      try {
        const response = await api.delete(`/books/${id}`);
        if (response.status === 204) {
          alert('Libro eliminado');
          this.fetchBooks();
        } else alert('Error al eliminar el libro');
      } catch (error) { alert('Error en el servidor'); }
    },
    cancelForm() { this.resetForm(); },
    resetForm() {
      this.newBook = { title: '', description: '', pageCount: 0, excerpt: '', publishDate: '' };
      this.CreateForm = false;
      this.editingBook = null;
      this.bookDetails = null; // Reiniciar detalles del libro
    }
  }
};
</script>

<style scoped>
.container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  font-family: Arial, sans-serif;
  background-color: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.title {
  text-align: center;
  color: #333;
}

.button {
  background-color: #007BFF;
  color: white;
  border: none;
  padding: 10px 15px;
  border-radius: 5px;
  cursor: pointer;
  margin: 10px 0;
}

.button:hover {
  background-color: #0056b3;
}

.create-button {
  margin-top: 20px;
}

.book-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

.book-table th, .book-table td {
  border: 1px solid #ddd;
  padding: 10px;
  text-align: left;
}

.book-table th {
  background-color: #f2f2f2;
  color: #333;
}

.book-item {
  transition: background-color 0.2s;
}

.book-item:hover {
  background-color: #f9f9f9;
}

.book-actions {
  display: flex;
  gap: 10px;
}

.action-button {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 5px 10px;
  border-radius: 3px;
}

.action-button:hover {
  background-color: #218838;
}

.delete-button {
  background-color: #dc3545;
}

.delete-button:hover {
  background-color: #c82333;
}

.form-container {
  margin-top: 20px;
  padding: 15px;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.form-title {
  margin-bottom: 15px;
}

.form-input,
.form-textarea {
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.form-textarea {
  resize: vertical;
}

.submit-button,
.cancel-button {
  width: 48%;
}

.details-container {
  margin-top: 20px;
  padding: 15px;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.details-title {
  margin-bottom: 15px;
}
</style>
