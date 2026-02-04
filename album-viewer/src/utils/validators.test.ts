import { describe, expect, it } from 'vitest';
import { validateDate, validateIPV6 } from './validators';


//test the validateDate function
describe('validateDate', () => {
  it('should return true for valid GUIDs', () => {
    const validGuids = [
      '550e8400-e29b-41d4-a716-446655440000',
      '123e4567-e89b-12d3-a456-426614174000',
      '00000000-0000-0000-0000-000000000000',
      'ffffffff-ffff-ffff-ffff-ffffffffffff',
    ];

    validGuids.forEach(guid => {
      expect(validateDate(guid)).toBe(true);
    });
  });

  it('should return false for invalid GUIDs', () => {
    const invalidGuids = [
      '',
      'not-a-guid',
      '550e8400e29b41d4a716446655440000', // missing dashes
      '550e8400-e29b-41d4-a716-44665544000Z', // invalid character
      '550e8400-e29b-41d4-a716-44665544000', // too short
      '550e8400-e29b-41d4-a716-4466554400000', // too long
      null as unknown as string,
      undefined as unknown as string,
      12345 as unknown as string,
    ];

    invalidGuids.forEach(guid => {
      expect(validateDate(guid)).toBe(false);
    });
  });
});
