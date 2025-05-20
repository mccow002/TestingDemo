import { TestBed } from '@angular/core/testing';

import { CatalogueHttpService } from './catalogue-http.service';

describe('CatalogueHttpService', () => {
  let service: CatalogueHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CatalogueHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
