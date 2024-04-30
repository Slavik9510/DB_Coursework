import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { Review } from '../_models/review.model';
import { StarRatingInputComponent } from '../star-rating-input/star-rating-input.component';

@Component({
  selector: 'app-review-input',
  templateUrl: './review-input.component.html',
  styleUrls: ['./review-input.component.css']
})
export class ReviewInputComponent {
  rating: number | undefined;
  content: string | undefined;
  @Output() reviewAdded = new EventEmitter();
  @ViewChild(StarRatingInputComponent) starRatingInput: StarRatingInputComponent | undefined;

  constructor() { }

  setRating(rating: number) {
    this.rating = rating;
  }

  addReview() {
    if (!this.rating) return;

    const review: Review = {
      reviewer: 'You',
      reviewDate: new Date(),
      rating: this.rating,
      content: this.content
    }
    this.reviewAdded.emit(review);

    this.content = undefined;
    this.rating = undefined;
    this.starRatingInput?.resetRating();
  }
}
