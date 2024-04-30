import { Component, OnInit } from '@angular/core';
import { InventoryItem } from '../_models/inventory-item.model';
import { PaginationParams } from '../_models/pagination-params';
import { InventoryService } from '../_services/inventory.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  items: InventoryItem[] = [];
  paginationParams: PaginationParams | undefined;
  totalItems: number | undefined;

  constructor(private inventoryService: InventoryService) { }

  ngOnInit(): void {
    this.paginationParams = new PaginationParams();

    this.inventoryService.getItemsToOrder(this.paginationParams).subscribe(response => {
      if (response.body)
        this.items = response.body;

      const paginationHeader = response.headers.get('Pagination');

      if (paginationHeader) {
        const paginationData = JSON.parse(paginationHeader);
        this.totalItems = paginationData.totalItems;
      }
    });
  }

  getTotalPages() {
    if (this.paginationParams && this.totalItems)
      return Math.ceil(this.totalItems / this.paginationParams?.pageSize);

    return null;
  }

  nextPage() {
    if (!this.paginationParams) return;

    this.paginationParams.pageNumber++;

    this.inventoryService.getItemsToOrder(this.paginationParams).subscribe(response => {
      if (response.body)
        this.items = response.body;
    });
  }

  previousPage() {
    if (!this.paginationParams) return;

    this.paginationParams.pageNumber--;

    this.inventoryService.getItemsToOrder(this.paginationParams).subscribe(response => {
      if (response.body)
        this.items = response.body;
    });
  }

  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
