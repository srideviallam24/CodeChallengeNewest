import { TestBed } from '@angular/core/testing';

import { NewStoriesServiceService } from './new-stories-service.service';

describe('NewStoriesServiceService', () => {
  let service: NewStoriesServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NewStoriesServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
