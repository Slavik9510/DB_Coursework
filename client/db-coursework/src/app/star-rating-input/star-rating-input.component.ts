import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-star-rating-input',
  templateUrl: './star-rating-input.component.html',
  styleUrls: ['./star-rating-input.component.css']
})
export class StarRatingInputComponent {
  currentRating = 0;
  hovered = 0;
  @Output() setRating = new EventEmitter();

  constructor() { }

  onClick(rating: number): void {
    this.currentRating = rating;

    this.setRating.emit(this.currentRating);
  }

  onHover(rating: number): void {
    this.hovered = rating;
  }

  onHoverLeave(): void {
    this.hovered = 0;
  }

  resetRating() {
    this.currentRating = 0;
  }
}
